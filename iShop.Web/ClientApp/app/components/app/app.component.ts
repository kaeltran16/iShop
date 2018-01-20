import { Component, OnDestroy, OnInit, HostListener} from '@angular/core';
import { SharedService } from '../../service/shared-service';
import { Cart } from "../../model/Cart";
import {ShoppingCart } from "../../model/ShoppingCart";

import { UserService } from '../../service/user.service';



@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
   
})
export class AppComponent implements OnInit, OnDestroy {
//    @HostListener('window:beforeunload', ['$event'])
//    public beforeunloadHandler($event: any) {
//        
//        $event.returnValue = "Bạn vẫn chưa thanh toán ...";
//        this.shoppingCartService.createShoppingCart("16122587-b163-4d90-d129-08d55a691528").subscribe(s => console.log(s));
//        return "Bạn vẫn chưa thanh toán ...";
//
//    }

   

//    @HostListener('window:unload', ['$event'])
//    unloadHandler($event:any) {
//       
//     
//        this.shoppingCartService.createShoppingCart("16122587-b163-4d90-d129-08d55a691528").subscribe(s => console.log(s));
//        $event.returnValue = "Are you sure?";
//    }

//    @HostListener('window:beforeunload', ['$event'])
//    beforeunloadHandler(event:any) {
//        alert("hoi");
//    }

  
    ngOnInit(): void {
//        window.onbeforeunload = (e) => {
//
//            this.shoppingCartService.createShoppingCart("16122587-b163-4d90-d129-08d55a691528").subscribe(s => console.log(s));
//        
//            return e.returnValue = "ban có muốn lưu thông tin không";
//        };
       
//        this.shoppingCartService.createShoppingCart("16122587-b163-4d90-d129-08d55a691528").subscribe(s => console.log(s));
        // when user close or refress browser  , save shopping cart 
//       
//        window.onunload = () => {
//          
//          localStorage.setItem("token","jsjsj");
//            this.shoppingCartService.createShoppingCart("16122587-b163-4d90-d129-08d55a691528").subscribe(s => console.log(s));
//
//
//        }
        

//   this.shoppingCartService.createShoppingCart("16122587-b163-4d90-d129-08d55a691528").subscribe(s => console.log(s));




        this.sharedService.changeEmitted$.subscribe(
            info => {
                this.infoCart = info;
            });

    }
    carts:Cart[]=[];
    infoCart:any;
    shoppingCart:ShoppingCart;
    constructor(
        private sharedService: SharedService,
    
        private userService: UserService
    ) {
    
    }


  
    ngOnDestroy(): void {
//        localStorage.setItem("token", "hoi");
    }
}
