import { Component, Output, EventEmitter, Input } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    animations: [
      
      
    ]
})
export class LoginComponent {
    @Output('onclick') onclick = new EventEmitter<boolean>();
    exit() {
        this.onclick.emit(false);
    }
}
