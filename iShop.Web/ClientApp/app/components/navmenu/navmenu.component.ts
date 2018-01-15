import { Component, TemplateRef,OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
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
       
    ]

})
export class NavMenuComponent implements OnInit{
    ngOnInit() {
        this.isShow = false;
    }
    userName:string="Đăng nhập";
    isShow: boolean = false;
    logged:boolean=false;
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

    modalRef: BsModalRef;
    constructor(private modalService: BsModalService,
        private route: ActivatedRoute,
        private router: Router
    ) {
       
    }
    exitLogin(isLogin: any) {
        
        if (isLogin.login) {
            this.modalRef.hide();
            this.userName = isLogin.userName;
            this.logged = true;
        }
        
    }
    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template);
    }

    openShoppingCart() {
//        this.router.navigate(['/shopping-cart']);
        this.isShow = true;
    }
    closeShoppingCart() {
      
        this.isShow = false;
    }
  
}
