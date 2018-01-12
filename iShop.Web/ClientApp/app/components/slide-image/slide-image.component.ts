import { Component, Input } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';

@Component({
    selector: 'slide-image',
    templateUrl: './slide-image.component.html',
    styleUrls: ['./slide-image.component.css'],
    animations: [
        trigger('show', [

            transition(':enter', animate('2000ms ease-in', keyframes([
                style({ transform: 'scale(1.3)', offset: 0 }),
                style({ transform: 'scale(1)', offset: 0.5 }),


            ])))
            ,

            transition(':leave', animate('1000ms ease-out', keyframes([
                style({ transform: 'scale(1)', offset: 0 }),
                style({ transform: 'scale(0)', offset: 0.5 }),

            ])))

        ]),
        trigger('slide', [

            transition(':enter', animate('2000ms ease-in', keyframes([
                style({ transform: 'translateX(-1000px)', offset: 0 }),
                style({ transform: 'translateX(0px)', offset: 0.5 }),


            ])))
            ,

            transition(':leave', animate('1000ms ease-out', keyframes([
                style({ transform: 'scale(1)', offset: 0 }),
                style({ transform: 'scale(0)', offset: 0.5 }),

            ])))

        ])
    ]
})
export class SlideImageComponent {
   

}
