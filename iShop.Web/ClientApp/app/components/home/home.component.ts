
import {  Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Rx';

import 'rxjs/add/observable/combineLatest';
@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})



export class HomeComponent implements OnInit {
 
    ticks = 0;
    timer: Observable<number>;
    ngOnInit() {
       
    }

    click() {
        
        this.timer = Observable.timer(1000, 1000);
        this.timer.subscribe(t => this.ticks = t);
    }
    constructor() { }

  
}
