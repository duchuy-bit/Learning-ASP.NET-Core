# Learning-API.NET-Core

+ Connect My SQL Server
+ Create API GET, POST, PUT, DELETE to Query Data
+ Create API Authentication
+ Create API Authorization


 ## Code First 
 - Sau khi con xong DB Context
`Tools` -> `Nuget Packet Manager` -> `Packet Manager Console`

Tạo Entities

```
Add-Migration AddTblLoai
```

Remove đi nếu sai

```
Remove-Migration AddTblLoai
```


- Sau khi đã chắn chắn thì mới update Database, vì đã update databse thì không thể remove Migration được nữa
  
```
update-database
```



## Database First 

- Database đã được tạo sẵn -> Ta chỉ lấy ra để sử dụng
-> Tạo folder Entities
-> Sau đó Gõ dòng lệnh Với 
   + Đường dẫn kết nối với DB: `Data Source=ADMIN\DUCHUY;Initial Catalog=MyShop;Integrated Security=True;TrustServerCertificate=True`
   + Folder Lưu trữ các bảng được tạo: `Entites`
 
```
Scaffold-DbContext "Data Source=ADMIN\DUCHUY;Initial Catalog=MyShop;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities
```


- Khi database Update -> Ta muốn ghi đè lại folder Entities ( `-f` *sau cùng* )


```
Scaffold-DbContext "Data Source=ADMIN\DUCHUY;Initial Catalog=MyShop;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -f
```

  ---------------------------------------------------------------
  DEV PANDA
  ---------------------------------------------------------------
