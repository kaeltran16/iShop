import { Component } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';
import { User } from "../../model/User";
import { UserService } from '../../service/user.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
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
    street: string;
    city:string;
    email:string;


    constructor(private userService: UserService, private route: ActivatedRoute,
        private router: Router) {
        
    }

    register() {
        var user = new User(this.firstName, this.lastName, this.password, this.email, this.street, this.city);
        this.userService.createUser(user);
        alert("Đăng kí thành công");
        this.router.navigate(['/home']);

    }
}
