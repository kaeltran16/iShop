import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { User} from "../model/User";
import { Login } from "../model/User";

@Injectable()
export class UserService {
    url: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.url = baseUrl;
    }
   createUser(user:User) {
       return this.http.post(this.url + '/api/Accounts/register', user);
   }

   login(login: Login) {
       return this.http.post(this.url + '/connect/token', login);
   }


}