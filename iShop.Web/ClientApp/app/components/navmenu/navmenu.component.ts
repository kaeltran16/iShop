import { Component } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    animations: [
        trigger('myAwesomeAnimation', [
            state('small', style({
                transform: 'scale(0.7)',
            })),
            state('large', style({
                transform: 'scale(1)',
            })),
            transition('small <=> large', animate('2000ms ease-in'))
        ]),
        trigger('show', [

            transition(':enter', animate('1000ms ease-in', keyframes([



            ])))
            ,

            transition(':leave', animate('1000ms ease-out', keyframes([
                style({ transform: 'scale(1)', offset: 0 }),
                style({ transform: 'scale(0)', offset: 0.5 }),

            ])))

        ])
    ]

})
export class NavMenuComponent {
    meet: any[] = [
        "Thịt Heo",
        "Thịt Bò",
        "Thịt Gà và Trứng"
    ];
    seaFood: any[] = [
        "Cá Đồng",
        "Cá Biển",
        "Các Loại Thủy Hải Sản Khác"
    ];
    hasLogin: boolean = true;
    login: boolean = false;
    register: boolean = false;
    exitLogin(isLogin: boolean) {
        this.login = isLogin;
    }
    exitRegister(isRegister: boolean) {
        this.register = isRegister;
    }
}
