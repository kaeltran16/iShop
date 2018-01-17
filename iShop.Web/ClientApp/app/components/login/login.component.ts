import { Component, Output, EventEmitter, Input } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';
import { UserService } from '../../service/user.service';
import { Login } from "../../model/User";
import { Observable } from 'rxjs/Rx';
@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    animations: [
      
      
    ]
})
export class LoginComponent {
    username: string="";
    password: string = "";
    result:boolean=false;
    @Output('onclick') onclick = new EventEmitter<any>();
   

        
 
    constructor(private userService: UserService) {
        
    }
    //login
    login($event: any) {
        if ($event.valid) {
            var user = new Login(this.username, this.password);
            //get token
            var token = this.userService.login(user);

            token.subscribe(t => {
                localStorage.setItem("token", t.access_token);
                   
                this.userService.info(t.access_token).subscribe(t => {
                        //output
                        this.onclick.emit({ login: true,userName:t.firstName });
                        
                        
                });
                    
                }, error => this.result = true // show label error when login fail 
            );

        }
    }

}
