 export class User {
   
   public  password : string;
     public firstName: string;
     public lastName: string;
     public email: string;
     public ward: string;
     public district:string;
    constructor(firstName: string, lastName: string, password: string,email: string, ward: string, district: string) {
         this.password = password;
         this.firstName = firstName;
         this.lastName = lastName;
         this.email = email;
         this.ward = ward;
         this.district = district;
     }



 }


export class Login {
    public username: string;
    public password: string;
    public grant_type: string;
    constructor(username:string, password:string) {
        this.grant_type = "password";
        this.username = username;
        this.password = password;
    }
}