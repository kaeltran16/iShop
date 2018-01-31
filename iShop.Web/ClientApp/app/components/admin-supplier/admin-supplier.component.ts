 
import { Component, OnInit,Input, TemplateRef } from '@angular/core';
import * as _ from 'underscore';
import { ActivatedRoute } from '@angular/router';
import 'rxjs/add/operator/map'
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import { PagerService } from '../../service/page.service';
import { ProductService } from '../../service/product.service';
import { OrderService } from '../../service/order.service';
import { UserService } from '../../service/user.service';
@Component({
    
    selector: 'admin-supplier',
    templateUrl: './admin-supplier.component.html',
    styleUrls: ['./admin-supplier.component.css']
})
export class AdminSupplierComponent implements OnInit {
    modalRef: BsModalRef;
    orders:any[]=[];
    constructor(
        private orderService: OrderService,
        private route: ActivatedRoute,
        private modalService: BsModalService,
        private productService: ProductService,
        private userService: UserService,
     
    ) { }
   
   

    ngOnInit() {
        let token = localStorage.getItem("token");
        token ? this.orderService.getOrders(token).subscribe(o => {
                _.each(o,
                    (os:any) => {
                        if (os.userId) {
//                            this.userService.
                        }
                    });
            }
            ): alert("Bạn không đủ quyền truy cập vào mục này");
    }


    exitDetail(isExit: boolean) {
        if (isExit) {
            this.modalRef.hide();
         
        }
    }

  

}
