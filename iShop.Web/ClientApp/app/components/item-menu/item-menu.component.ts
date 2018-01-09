import { Component,Input } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';

@Component({
    selector: 'item-menu',
    templateUrl: './item-menu.component.html',
    styleUrls: ['./item-menu.component.css'],
    animations: [
       
        trigger('show', [

            transition(':enter', animate('2000ms ease-in', keyframes([
                style({ opacity: 0, transform: 'scale(0.7)', offset: 0 }),
                style({ opacity: 1, transform: 'scale(1)', offset: 0.5 }),

            ])))
            ,

            transition(':leave', animate('2000ms ease-out', keyframes([
                style({ opacity: 0, transform: 'scale(1)', offset: 0 }),
                style({ opacity: 1, transform: 'scale(0)', offset: 0.5 }),

            ])))

        ])
    ]
})
export class ItemMenuComponent {

    @Input('title') title: any;
    @Input('categories') categories:any[];
    is: boolean = false;
    hover() {
        this.is = true;
    }
    leave() {
        this.is = false;
    }
    link: string = "more-product";
}
