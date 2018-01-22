import { Component, Output, EventEmitter} from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';
import { User } from "../../model/User";
import { UserService } from '../../service/user.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import 'rxjs/add/operator/catch';

import { Login } from "../../model/User";
import { SharedService } from '../../service/shared-service';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css'],
    animations: [
       
      
    ]
})
export class RegisterComponent {
    firstName: string;
    lastName: string;
    password: string;
    confirmPassword: string;
    telephone: string;
    ward: string;
    district:string;
    email: string;
    result:boolean=false;
    @Output('onclick') onclick = new EventEmitter<boolean>();// when click login then  hidden dialog 

    constructor(private userService: UserService, private route: ActivatedRoute, private sharedService: SharedService,
        private router: Router) {
      
    }
    login() {

        var user = new Login(this.email, this.password);
        //get token
        var token = this.userService.login(user);
        setTimeout(() => {
            localStorage.removeItem("token");
            this.sharedService.emitChangeToken({ "lastName": "Đăng Nhập" });
        }, 1800000);
        token.subscribe(t => {

                localStorage.setItem("token", t.access_token);
                this.userService.info(t.access_token).subscribe(t => {
                    //output
                    this.onclick.emit(true);
                    this.sharedService.emitChangeToken(t);
                });
            

            }, error => this.result = true // show label error when login fail 
        );


    }
    async register($event:any) {
        var user = new User(this.firstName, this.lastName, this.password, this.email, this.ward, this.district, this.telephone);
        this.result = false;
        //create user
        this.userService.createUser(user).subscribe(p => {
                if ($event.valid) {
                    {
                        this.router.navigate(['/home']);
                        this.onclick.emit(true);
                        this.login();
                    }

                }
            },
            (err: any) => this.result = true
        );

        

    }


    
}
