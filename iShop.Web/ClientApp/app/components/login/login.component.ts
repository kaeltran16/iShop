import { Component, Output, EventEmitter, Input } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';
import { UserService } from '../../service/user.service';
import { Login } from "../../model/User";
@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    animations: [
      
      
    ]
})
export class LoginComponent {
    username: string="";
    password: string="";

    constructor(private userService: UserService) {
        
    }
    
    login() {
        var user = new Login(this.username, this.password);
        var token = this.userService.login(user);
        token.map(t=>console.log(t.ok));
        console.log(this.username+ " "+this.password);
    }

}
