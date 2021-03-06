# 1. List of technologies:
   - Technologies used :
     - Visual studio 2015
     - C# 6
     - ASP.NET MVC 5 (Web Application)
     - .Net Framework 4.6
     - ASP.NET Web Api ( Web service )
     - Mongo DB Server
     - MongoDb-CSharp driver
     - NuGet package manager
       
  
   - Libraries mandatory used in Web Application :
     Install labiraries from Package Management Console and all package target framework 4.6 :
     1. Install-Package MongoDB.Bson -Version 2.3.0
     2. Install-Package MongoDB.Driver -Version 2.3.0
     3. Install-Package MongoDB.Driver.Core -Version 2.3.0
     4. Install-Package mongocsharpdriver -Version 2.3.0
     5. Install-Package PagedList -Version 1.17.0
     6. Install-Package PagedList.Mvc -Version 4.5.0
     7. Install-Package System.Runtime.InteropServices.RuntimeInformation
     
 ## 2. Architecture/Design implementation
    
  Member ( user ) can be books demand and maintain , therefor must be relation to store user and book data in same relation as in image following:
![1](https://user-images.githubusercontent.com/23058510/127831694-226d94b9-e831-43ce-a7d9-f70d50214cae.JPG)

Architecture
![2](https://user-images.githubusercontent.com/23058510/127832549-e6ab8211-9cf3-4dde-9f30-a32d8859b9d9.JPG)
![3](https://user-images.githubusercontent.com/23058510/127832566-f3dde231-f570-4c81-ae88-b64e86b76183.JPG)


   - Description functional to web application :
     - Home page : show list all books , can be search it from same page and go to detail book.
     - Login page.
     - Register page : username is unique key and password more 4 characters.
     - If user login : Show pages following:
       - Logout
       - List books demands to user and can be remove book from list demands from same page
       - User can be add book to list demands from book page
     - Web service : Create Web Api as web service
       
# 3. Diagrams:
## 3.1 Class Diagram:
![4](https://user-images.githubusercontent.com/23058510/127833564-ad442011-a0a5-480e-a726-6e3a357eaf5b.JPG)

## 3.2 Activity Diagram:
![5](https://user-images.githubusercontent.com/23058510/127833614-7f2878fc-f8e3-4c79-9aa8-430821054263.JPG)
![6](https://user-images.githubusercontent.com/23058510/127833666-268caafb-8247-4c33-bbb5-9993b713932d.JPG)

## 3.3 Sequence Diagram:
![7](https://user-images.githubusercontent.com/23058510/127833875-74ad9c19-f700-4479-88e1-43cf7a6384fd.JPG)

