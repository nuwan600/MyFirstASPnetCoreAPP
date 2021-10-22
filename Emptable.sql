Create procedure spAddEmployee     
(    
    @FirstName VARCHAR(150),
	@LastName VARCHAR(150),     
    @City VARCHAR(20),    
    @Department VARCHAR(20),    
    @Gender VARCHAR(6),
	@IsDeleted bit    
)    
as     
Begin     
    Insert into Employee (FirstName,LastName,City,Department,Gender,IsDeleted)     
    Values (@FirstName,@LastName,@City,@Department, @Gender,@IsDeleted)     
End
Go
Create procedure spUpdateEmployee      
(      
   @EmpId INTEGER ,    
   @FirstName VARCHAR(150),
   @LastName VARCHAR(150),     
   @City VARCHAR(20),    
   @Department VARCHAR(20),    
   @Gender VARCHAR(6),
   @IsDeleted bit   
)      
as      
begin      
   Update Employee       
   set 
	   FirstName=@FirstName,
	   LastName=@LastName,     
	   City=@City,      
	   Department=@Department,    
	   Gender=@Gender,
	   IsDeleted=@IsDeleted     
   where EmployeeID=@EmpId      
End
GO
Create procedure spDeleteEmployee     
(      
   @EmpId int,
   @IsDeleted bit      
)      
as       
begin      
  Update Employee       
   set
	   IsDeleted=@IsDeleted     
   where EmployeeID=@EmpId            
End
Go
Create procedure spGetAllEmployees    
as    
Begin    
    select *    
    from Employee    
End

