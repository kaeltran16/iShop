 export class User {
   
   private password : string;
   private firstName: string;
   private lastName: string;
   private email: string;
   private street: string;
    private city:string;
    constructor(firstName: string, lastName: string, password: string,email: string, street: string, city: string) {
         this.password = password;
         this.firstName = firstName;
         this.lastName = lastName;
         this.email = email;
         this.street = street;
         this.city = city;
     }
   
}


export class Login {
    private username: string;
    private password: string;
    private grant_type: string;
    constructor(username:string, password:string) {
        this.grant_type = "password";
        this.username = username;
        this.password = password;
    }
}