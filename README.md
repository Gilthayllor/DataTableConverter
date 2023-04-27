# DataTableConverter
`DataTableConverter` is a C# library for converting DataTables to a user-defined model List, or vice versa.

| Package |  Version
| ------- | ----- |
| `DataTableConverter` | [![NuGet](https://img.shields.io/nuget/v/DataTableConverter.svg)](https://nuget.org/packages/DataTableConverter) | [![Nuget](https://img.shields.io/nuget/dt/DataTableConverter.svg)](https://nuget.org/packages/DataTableConverter) |


### Dependencies
.NET Standard 2.0

You can check supported frameworks here:

https://docs.microsoft.com/pt-br/dotnet/standard/net-standard

### Instalation
This package is available through Nuget Packages: https://www.nuget.org/packages/DataTableConverter


**Nuget**
```
Install-Package DataTableConverter
```

**.NET CLI**
```
dotnet add package DataTableConverter
```

### Usage
To use `DataTableConverter`, first create an instance of the appropriate converter class:

```csharp
DataTableToObjectConverter<MyModel> dataTableToObjectConverter = new DataTableToObjectConverter<MyModel>();

ObjectToDataTableConverter<MyModel> objectToDataTableConverter = new ObjectToDataTableConverter<MyModel>();
```
### Attributes
This library provides two attributes to customize the conversion process:

##### `[DataTableColumn("ColumnName")]`
Use this attribute to specify the name of the column in the `DataTable` that should be used to populate the corresponding property in the custom model. 
If this attribute is not used, the name of the property will be used by default.

Example usage:
```csharp
public class MyCustomModel
{
    [DataTableColumn("FirstColumn")]
    public string MyProperty { get; set; }
}
```
##### `[ExcludeFromDataTable]`
Use this attribute to indicate that a property in the custom model should not be included in the `DataTable` conversion. 
Any property with this attribute will be excluded from the resulting `DataTable`.

Example usage:
```csharp
public class MyCustomModel
{
    public string MyProperty { get; set; }

    [ExcludeFromDataTable]
    public int MyExcludedProperty { get; set; }
}
```

### Convert DataTable to List
To convert a `DataTable` to a List of a user-defined model, use the `Convert` method of the `DataTableToObjectConverter`:

```csharp
DataTable table = GetDataTable(); // Replace with your own DataTable
List<MyModel> list = dataTableToObjectConverter.Convert(table);

// or convert a single row

MyModel model = dataTableToObjectConverter.Convert(table.Rows[0]);
```

### Convert List to DataTable
To convert a List of a user-defined model to a DataTable, use the `Convert` method of the `ObjectToDataTableConverter`:

```csharp
List<MyModel> list = GetMyModelList(); // Replace with your own List<MyModel>

DataTable table = objectToDataTableConverter.Convert(list);

// Alternatively, to convert a single item into a DataRow, 
// you will need to pass a DataTable as a parameter to properly generate the DataRow.

MyModel model = new MyModel(); // Replace with your own model;

DataRow row = objectToDataTableConverter.Convert(table, model);
```
### License
DataTableConverter is released under the MIT License. See [LICENSE](https://opensource.org/license/mit/) for more information.
