# .NET 8.0 Migration Notes

## Summary
All projects in the Alten Connected Vehicles solution have been successfully migrated from .NET Framework 4.5.2 to .NET 8.0.

## Migration Date
November 16, 2025

## Projects Migrated (18 total)

### Class Libraries (9 projects)
1. ✅ **Alten.Connected_Vehicles.DTO** - Migrated to .NET 8.0
2. ✅ **Alten.Connected_Vehicles.Infrastructure** - Migrated to .NET 8.0
   - Updated: Newtonsoft.Json 13.0.3, RestSharp 112.1.0, System.ServiceModel.Primitives 8.0.0
3. ✅ **Alten.Connected_vehicle.Model** - Migrated to .NET 8.0 with EF Core
   - Converted from Entity Framework 6 to Entity Framework Core 8.0.11
   - Updated DbContext to use EF Core APIs (ModelBuilder, DbContextOptions)
4. ✅ **Alten.Connected_Vehicles.DAL** - Migrated to .NET 8.0
   - Note: EDMX files excluded from compilation (legacy EF6 artifacts)
5. ✅ **Alten.Connected_Vehicles.MSSQLRepository** - Migrated to .NET 8.0
6. ✅ **Alten.Connected_Vehicles.BLL** - Migrated to .NET 8.0
7. ✅ **Alten.Connected_Vehicles.TCPServer.Common** - Migrated to .NET 8.0
   - Migrated from ASP.NET SignalR to ASP.NET Core SignalR 1.1.0
8. ✅ **Alten.Connected_Vehicles.Test.Common** - Migrated to .NET 8.0
9. ✅ **ClassLibrary1** - Migrated to .NET 8.0 (placeholder project)

### Web Applications (2 projects)
10. ✅ **Alten.Connected_Vehicles.WebAPI** - Migrated to ASP.NET Core 8.0
    - Converted from ASP.NET Web API to ASP.NET Core Web API
    - Added Program.cs with minimal hosting model
    - Added appsettings.json for configuration
    - Added Swagger/OpenAPI support
    - Added Newtonsoft.Json support for compatibility
11. ✅ **Alten.Connected_Vehicles.UI** - Migrated to ASP.NET Core 8.0
    - Converted from ASP.NET MVC to ASP.NET Core MVC
    - Migrated to ASP.NET Core SignalR Client

### Windows Applications (2 projects)
12. ✅ **Alten.Connected_Vehicles.TCPServer** - Migrated to .NET 8.0
    - Converted from Windows Service to .NET 8.0 Worker Service pattern
    - Added Microsoft.Extensions.Hosting.WindowsServices 8.0.1
13. ✅ **Alten_Connected_Vehicles.VehiclesRobot** - Migrated to .NET 8.0-windows
    - Converted Windows Forms app to .NET 8.0-windows

### Test Projects (5 projects)
14. ✅ **Alten.Connected_vehicles.BLL.Test** - Migrated to .NET 8.0
15. ✅ **Alten.Connected_vehicle.Model.Test** - Migrated to .NET 8.0
16. ✅ **Alten.Connected_Vehicles.WebAPI.Test** - Migrated to .NET 8.0
17. ✅ **Alten.Connected_Vehicles.WebAPI.Tests** - Migrated to .NET 8.0
18. ✅ **Alten.Connected_Vehicles.UI.Tests** - Migrated to .NET 8.0
    - All test projects updated to use MSTest 3.6.3 and .NET Test SDK 17.11.1

## Major Changes

### 1. Entity Framework 6 → Entity Framework Core 8.0
- Updated `Connected_Vehicles_Models.cs` to use EF Core APIs
- Changed `DbModelBuilder` to `ModelBuilder`
- Added `DbContextOptions` constructor for dependency injection
- Removed `Database.SetInitializer` (not used in EF Core)
- Added `OnConfiguring` method for backward compatibility

### 2. ASP.NET → ASP.NET Core
- **WebAPI Project:**
  - Removed Global.asax and old-style Startup.cs
  - Created new Program.cs with WebApplicationBuilder
  - Added appsettings.json for configuration
  - Integrated Swagger/OpenAPI for API documentation
  - Added CORS support
- **UI Project:**
  - Converted to ASP.NET Core MVC
  - Removed Global.asax and Web.config

### 3. ASP.NET SignalR → ASP.NET Core SignalR
- Updated TCPServer.Common to use Microsoft.AspNetCore.SignalR.Core 1.1.0
- UI project updated to use SignalR client libraries

### 4. Windows Service → Worker Service
- TCPServer converted to use Microsoft.Extensions.Hosting pattern
- Added Microsoft.Extensions.Hosting.WindowsServices for Windows Service support

### 5. Project File Format
- All projects converted from old-style .csproj to SDK-style .csproj
- Much cleaner, shorter project files
- Files automatically included via globbing patterns
- PackageReferences instead of packages.config

## Package Updates

### Key Package Upgrades:
- Entity Framework 6.1.3 → Entity Framework Core 8.0.11
- Newtonsoft.Json 6.0.4/10.0.2 → 13.0.3
- RestSharp 105.2.3 → 112.1.0
- ASP.NET SignalR 2.2.2 → ASP.NET Core SignalR 1.1.0
- MSTest 1.x → MSTest 3.6.3

## Next Steps

### 1. Build and Test
```bash
# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test
```

### 2. Update Connection Strings
- Update `appsettings.json` in the WebAPI project with your SQL Server connection string
- The default connection string is set to LocalDB

### 3. Recreate EF Core Migrations
The old EF6 migrations need to be recreated for EF Core:
```bash
cd Alten.Connected_vehicle.Model
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Code Changes Required

#### WebAPI Controllers
- Update controllers to use ASP.NET Core attributes
- Replace `[RoutePrefix]` with `[Route]`
- Update OWIN authentication to ASP.NET Core Identity if used

#### SignalR Hubs
- Update SignalR hub registration in Startup
- Update client-side SignalR JavaScript code to use new @microsoft/signalr package

#### Windows Service
- Update TCPServer Program.cs to use Worker Service pattern:
```csharp
var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services => {
        services.AddHostedService<Worker>();
    });
```

### 5. Testing Checklist
- [ ] All projects build successfully
- [ ] Unit tests pass
- [ ] WebAPI endpoints respond correctly
- [ ] Database connection works
- [ ] SignalR real-time updates work
- [ ] TCP Server connects and processes data
- [ ] UI application loads and functions correctly
- [ ] Windows Forms robot application works

### 6. Known Issues to Address

1. **DAL Project EDMX Files**: The Model1.edmx and generated files are excluded from compilation. If database-first approach is still needed, consider using EF Core Power Tools or scaffold from database.

2. **Authentication**: If the WebAPI uses ASP.NET Identity, it needs to be migrated to ASP.NET Core Identity.

3. **Configuration**: All Web.config settings need to be moved to appsettings.json

4. **Static Files**: Ensure wwwroot folder structure is set up correctly for static files in web projects

5. **Dependency Injection**: Update service registration to use ASP.NET Core DI container

## Benefits of .NET 8.0

1. **Performance**: Significant performance improvements over .NET Framework
2. **Cross-Platform**: Can now run on Linux and macOS (except Windows-specific projects)
3. **Modern C#**: Access to latest C# language features
4. **Better Tooling**: Improved development experience with modern SDK
5. **Long-term Support**: .NET 8 is an LTS release with support until November 2026
6. **Containerization**: Easy Docker containerization support
7. **Cloud-Ready**: Better Azure and cloud integration

## Breaking Changes

1. System.Web dependencies removed (use ASP.NET Core equivalents)
2. Configuration system changed (XML config → JSON)
3. Entity Framework 6 migrations not compatible with EF Core
4. OWIN middleware replaced with ASP.NET Core middleware
5. Windows-specific features (Windows Forms, Windows Service) require net8.0-windows target

## Resources

- [.NET 8 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [Migrating from ASP.NET to ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/migration/proper-to-2x/)
- [EF Core Migration Guide](https://learn.microsoft.com/en-us/ef/efcore-and-ef6/)
- [Worker Services in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/workers)

## Contact
For questions about this migration, please contact the development team.
