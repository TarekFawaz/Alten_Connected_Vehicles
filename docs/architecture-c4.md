# Alten Connected Vehicles — C4 Architecture

This document provides a C4-style view (Context, Container, and Component) of the current solution.

## 1) System Context (C4 Level 1)

```mermaid
C4Context
    title Alten Connected Vehicles - System Context

    Person(user, "Operations/User", "Monitors vehicles and their statuses")
    Person(device, "Vehicle Device / Robot", "Sends telemetry packets over TCP")

    System_Boundary(system, "Alten Connected Vehicles") {
        System(platform, "Connected Vehicles Platform", "Ingests device packets, processes status, persists data, and exposes live/UI views")
    }

    System_Ext(msmq, "MSMQ", "Message queue used for async packet processing")
    System_Ext(sql, "SQL Server", "Stores customers, vehicles, raw transactions and transactions")

    Rel(user, platform, "Views dashboard and queries data", "Browser / HTTP")
    Rel(device, platform, "Sends telemetry", "TCP")
    Rel(platform, msmq, "Queues incoming payloads", "MSMQ")
    Rel(platform, sql, "Reads/Writes domain data", "ADO.NET/EF")
```

## 2) Container Diagram (C4 Level 2)

```mermaid
C4Container
    title Alten Connected Vehicles - Container View

    Person(user, "Operations/User")
    Person(device, "Vehicle Device / Robot")

    System_Boundary(system, "Alten Connected Vehicles") {
        Container(ui, "UI Web App", "ASP.NET MVC + AngularJS", "Renders customers/vehicles and receives live SignalR updates")
        Container(webapi, "Web API", "ASP.NET Web API + OWIN", "CRUD/query endpoints for customers, vehicles, transactions")
        Container(tcp, "TCP Windows Service", "Windows Service + TCP Listener", "Accepts incoming telemetry packets")
        Container(queueWorker, "Queue Processor", "MSMQ consumer in TCPServer.Common", "Parses payloads, emits SignalR events, forwards data to Web API")
        Container(signalr, "SignalR Self Host", "OWIN SignalR Hub on :8088", "Push channel for live vehicle status updates")
        ContainerDb(db, "Alten_Connected_Vehicles DB", "SQL Server", "Persistent storage")
        ContainerQueue(msmq, "ATVS Queue", "MSMQ", "Internal async message queue")
    }

    Rel(user, ui, "Uses", "HTTPS")
    Rel(device, tcp, "Sends status packets", "TCP")
    Rel(tcp, msmq, "Enqueues packet bytes", "MSMQ")
    Rel(msmq, queueWorker, "Dequeues packet bytes")
    Rel(queueWorker, signalr, "Publishes live notification", "SignalR")
    Rel(queueWorker, webapi, "Posts transaction/raw payload", "HTTP")
    Rel(ui, webapi, "Reads customers/vehicles", "HTTP")
    Rel(ui, signalr, "Subscribes to live updates", "WebSocket/Long Polling")
    Rel(webapi, db, "Reads/Writes", "Repository + SQL")
```

## 3) Component Diagram for Web API (C4 Level 3)

```mermaid
C4Component
    title Alten Connected Vehicles - Web API Components

    Container_Boundary(webapi, "Web API Container") {
        Component(controllers, "API Controllers", "ASP.NET Web API", "Expose endpoints for customers, vehicles, transactions, raw transactions")
        Component(services, "Business Services", "BLL", "Business rules for vehicle/transaction/customer operations")
        Component(repo, "Repository + UnitOfWork", "MSSQLRepository", "Data access abstraction")
        Component(models, "Domain/DTO Models", "Model + DTO", "Transfer objects and core entities")
    }

    ContainerDb(db, "SQL Server DB", "SQL Server")

    Rel(controllers, services, "Calls")
    Rel(services, repo, "Uses")
    Rel(services, models, "Maps/validates")
    Rel(repo, db, "Persists/queries")
```

## Key Runtime Flow

1. A device (or the robot simulator) sends a packet to the TCP Windows Service.
2. The service pushes raw bytes to MSMQ (`ATVS` private queue).
3. MSMQ consumer parses packet data (registration + status), then:
   - publishes real-time status via SignalR hub,
   - posts transaction/raw data to the Web API.
4. Web API executes BLL logic and repository operations to persist transaction and update vehicle status.
5. UI fetches baseline customer/vehicle data from Web API and receives live status changes from SignalR.
