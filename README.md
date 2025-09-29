# SQL CRUD Application

A modern ASP.NET Core MVC application demonstrating CRUD operations with Entity Framework Core and SQLite database. This project features an attractive Bootstrap-based frontend, comprehensive product management functionality, and implements both Factory Pattern and Abstract Factory Pattern for product creation.

## 🚀 Features

- **Full CRUD Operations**: Create, Read, Update, and Delete products
- **Modern UI**: Responsive design with Bootstrap 5 and Font Awesome icons
- **Search & Filter**: Search products by name, description, or category
- **Data Validation**: Client and server-side validation
- **Entity Framework Core**: Code-first approach with migrations
- **SQLite Database**: Lightweight, file-based database
- **Design Patterns**: Factory Pattern and Abstract Factory Pattern implementation
- **Category-Based Management**: Organized product management by categories
- **Swagger/OpenAPI**: Complete API documentation with interactive testing
- **RESTful API**: Dedicated API endpoints for programmatic access

## 🛠️ Technologies Used

- **.NET 9.0**
- **ASP.NET Core MVC**
- **Entity Framework Core 9.0**
- **SQLite Database**
- **Bootstrap 5.3.2**
- **Font Awesome 6.4.0**
- **jQuery & jQuery Validation**
- **Swagger/OpenAPI 6.5.0**
- **Newtonsoft.Json**

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/)
- No additional database setup required (uses SQLite)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd SQLCRUD
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Install Client-Side Libraries

```bash
libman restore
```

### 4. Create and Update Database

```bash
# Add migration (if needed)
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The application will be available at:
- HTTP: `http://localhost:50001`
- HTTPS: `https://localhost:7000`
- **Swagger API Documentation**: `http://localhost:50001/api-docs` or `https://localhost:7000/api-docs`

## 📁 Project Structure

```
SQLCRUD/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   ├── ProductsController.cs
│   └── ProductsApiController.cs
├── Data/                 # Entity Framework DbContext
│   └── ApplicationDbContext.cs
├── Models/               # Data Models
│   ├── Product.cs
│   └── ProductDto.cs
├── Interfaces/           # Design Pattern Interfaces
│   ├── IProductService.cs
│   ├── IProductFactory.cs
│   ├── ICategoryCrudFactory.cs
│   ├── IAbstractProductFactory.cs
│   └── IAbstractProductService.cs
├── Factories/            # Factory Pattern Implementation
│   ├── ProductFactory.cs
│   ├── CategoryCrudFactory.cs
│   ├── AbstractProductFactory.cs
│   ├── ElectronicsFactory.cs
│   └── FurnitureFactory.cs
├── Services/             # Business Logic Services
│   ├── ProductService.cs
│   └── AbstractProductService.cs
├── Filters/              # Swagger Filters
│   └── ExampleSchemaFilter.cs
├── Views/                # Razor Views
│   ├── Home/
│   ├── Products/
│   └── Shared/
├── wwwroot/              # Static Files
│   ├── css/
│   ├── js/
│   └── lib/
├── Migrations/           # Entity Framework Migrations
├── Properties/
│   └── launchSettings.json
├── Program.cs            # Application Entry Point
├── appsettings.json      # Configuration
└── README.md
```

## 🎯 Key Features Explained

### Design Patterns Implementation

#### Factory Pattern
- **CategoryCrudFactory**: Handles CRUD operations for different product categories
- **ProductFactory**: Creates products with consistent structure
- **Category-based Operations**: Electronics, Furniture, Automobile, and Others

#### Abstract Factory Pattern
- **AbstractProductFactory**: Creates factories for different product families
- **ElectronicsFactory**: Creates mobile phones, laptops, and headphones
- **FurnitureFactory**: Creates sofas, tables, beds, and curtains
- **Specialized Product Creation**: Each product type has unique characteristics and emoji prefixes

### Product Management
- **Create**: Add new products with validation using Factory patterns
- **Read**: View all products with search and filtering
- **Update**: Edit existing product information
- **Delete**: Remove products with confirmation

### Search & Filtering
- Search by product name, description, or category
- Filter by category dropdown
- Clear filters functionality

### Responsive Design
- Mobile-first approach with Bootstrap
- Card-based layout for product display
- Interactive buttons with hover effects
- Font Awesome icons throughout

### Data Validation
- Required field validation
- Range validation for prices and quantities
- String length validation
- Client-side and server-side validation

### API Documentation (Swagger)
- **Interactive API Testing**: Test all endpoints directly from the browser
- **Comprehensive Documentation**: Detailed descriptions for all API endpoints
- **Request/Response Examples**: Sample data for all operations
- **Schema Definitions**: Complete data models with validation rules
- **HTTP Status Codes**: Clear documentation of all possible responses

### RESTful API Endpoints
- `GET /api/products` - Get all products with optional filtering
- `GET /api/products/{id}` - Get specific product by ID
- `GET /api/products/categories` - Get all available categories
- `GET /api/products/statistics` - Get product statistics summary
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update existing product
- `DELETE /api/products/{id}` - Delete product

## 🗄️ Database Schema

The `Product` model includes:
- `Id` (Primary Key)
- `Name` (Required, Max 100 chars)
- `Description` (Optional, Max 500 chars)
- `Category` (Required, Max 50 chars)
- `StockQuantity` (Integer, Min 0)
- `DateCreated` (DateTime)
- `IsActive` (Boolean)

**Note**: The Price field was removed in a recent update to simplify the model.

## 🎨 UI Components

### Home Page
- Statistics dashboard
- Recent products display
- Quick action buttons

### Products Index
- Grid layout with product cards
- Search and filter functionality
- Action buttons for each product

### Product Forms
- Clean, validated input forms
- Icon-enhanced input fields
- Success/error messaging

## 🔧 Configuration

### Connection String
The application uses SQLite by default:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=SQLCRUD_Dev.db"
  }
}
```

### Environment Settings
- Development: Uses `SQLCRUD_Dev.db` database file
- Production: Uses `SQLCRUD.db` database file

## 🔧 Using the API

### Swagger UI
1. Navigate to `http://localhost:50001/api-docs` in your browser
2. Explore the interactive API documentation
3. Click "Try it out" on any endpoint to test it
4. Use the provided examples or create your own requests

### Example API Calls

#### Get All Products
```bash
curl -X GET "http://localhost:50001/api/products" -H "accept: application/json"
```

#### Create a New Product
```bash
curl -X POST "http://localhost:50001/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "New Product",
    "description": "Product description",
    "category": "Electronics",
    "stockQuantity": 10,
    "isActive": true
  }'
```

#### Search Products
```bash
curl -X GET "http://localhost:50001/api/products?searchString=laptop&categoryFilter=Electronics"
```

#### Get Product Statistics
```bash
curl -X GET "http://localhost:50001/api/products/statistics"
```

## 🚀 Deployment

### For GitHub Pages (Static Hosting)
This is an ASP.NET Core application and requires a server environment. For static hosting alternatives, consider:
- Azure App Service
- AWS Elastic Beanstalk
- Heroku
- DigitalOcean App Platform

### Docker Support
To containerize the application:

1. Create a `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SQLCRUD.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SQLCRUD.dll"]
```

2. Build and run:
```bash
docker build -t sqlcrud .
docker run -p 8080:80 sqlcrud
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Bootstrap team for the amazing CSS framework
- Font Awesome for the beautiful icons
- Microsoft for ASP.NET Core and Entity Framework
- The .NET community for excellent documentation

## 🔧 Recent Updates & Fixes

### Version 2.0 - Design Patterns Implementation
- ✅ **Factory Pattern**: Implemented CategoryCrudFactory for category-based operations
- ✅ **Abstract Factory Pattern**: Added AbstractProductFactory for specialized product creation
- ✅ **SQLite Migration**: Switched from SQL Server LocalDB to SQLite for easier deployment
- ✅ **View Fixes**: Resolved CreateHeadphones and other create endpoints
- ✅ **Generic Views**: Implemented CreateAbstractProduct view for all product types
- ✅ **Category Management**: Enhanced category selection and management interface
- ✅ **Product Types**: Added specialized creation for Electronics and Furniture products
- ✅ **Port Configuration**: Updated to use port 50001 for HTTP and 7000 for HTTPS

### Bug Fixes
- Fixed CreateHeadphones endpoint returning 404 error
- Resolved file locking issues during development
- Updated all create action methods to use generic view
- Improved error handling and user feedback

## 📞 Support

If you encounter any issues or have questions:

1. Check the [Issues](https://github.com/yourusername/SQLCRUD/issues) page
2. Create a new issue with detailed information
3. Include your environment details (.NET version, OS, etc.)

---

**Happy Coding! 🎉**
