import { Injectable, Inject } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { User } from "../model/User";
import { Login } from "../model/User";

@Injectable()
export class UserService {
    url: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.url = baseUrl;
    }

    //create User 
    createUser(user: User) {
        //
        return this.http.post(this.url + 'api/Accounts/register',
            {
                firstname: user.firstName,
                lastname: user.lastName,
                password: user.password,
                email: user.email,
                ward: user.ward,
                district: user.district,
                phoneNumber: user.phoneNumber
    });


    }

   // Login with  username and password 
    login(login: Login) {

        return this.http.post(this.url + '/connect/token', "username=" + encodeURIComponent(login.username) +
            "&password=" + encodeURIComponent(login.password) +
            "&grant_type=password",
            ({ headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }) as any).map(t=>t.json());
    }

    logout(token: any) {
        return this.http.get(this.url + 'connect/logout',
            ({
                headers: {
                    //USE credentials mode
                    withCredentials: true,
                    'Authorization': 'Bearer ' + token
                }
            }) as any
        )
    }
    //Get info of User
    info(token: any) {
        return this.http.get(this.url + 'api/userinfo',
            ({
                headers: {
                    //USE credentials mode
                    withCredentials: true,
                    'Authorization': 'Bearer ' + token
                }
            }) as any
           ).map(u=>u.json());
    } 

}