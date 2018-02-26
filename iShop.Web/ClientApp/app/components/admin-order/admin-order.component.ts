 
import { Component, OnInit, Input, TemplateRef, Output,EventEmitter } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import 'rxjs/add/operator/map'
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ProductService } from '../../service/product.service';
import { OrderService } from '../../service/order.service';
import { PagerService } from '../../service/page.service';
@Component({
    
    selector: 'admin-order',
    templateUrl: './admin-order.component.html',
    styleUrls: ['./admin-order.component.css']
})
export class AdminOrderComponent implements OnInit {
    modalRef: BsModalRef;
    orders:any[]=[];
    constructor(
        private orderService: OrderService,
        private route: ActivatedRoute,
        private modalService: BsModalService,
        private productService: ProductService,
        private pagerService: PagerService
    ) { }
 
 
    // pager object
    pager: any = {};

    // paged items
    pagedItems: any[];
    

    ngOnInit() {
        let token = localStorage.getItem("token");
        token ? this.orderService.getOrders(token).subscribe(o => {
            this.orders = o.sort((n1: any, n2: any) => {
                
                if (n1.shipping.shippingState < n2.shipping.shippingState) return -1;
                else if (n1.shipping.shippingState > n2.shipping.shippingState) return 1;
                else return 0;
            });
                this.setPage(1);
            }
            ): alert("Bạn không đủ quyền truy cập vào mục này");
    }
    setPage(page: number) {
        if (page < 1 || page > this.pager.totalPages) {
            return;
        }

        // get pager object from service
        this.pager = this.pagerService.getPager(this.orders.length, page,50);

        // get current page of items
        this.pagedItems = this.orders.slice(this.pager.startIndex, this.pager.endIndex + 1);
    }


    exitDetail(id: any) {
        if (id) {
            this.modalRef.hide();
            if (typeof id === 'boolean') return;
            let index = this.pagedItems.findIndex(o => o.id === id);
           
            this.pagedItems.splice(index, 1);
        }
    }

    isdelete: boolean = false;
    openModal(template: TemplateRef<any>) {
        if (this.isdelete)
        { this.isdelete = false; return; }
        this.modalRef = this.modalService.show(template);
     

    }
  

    deleteOrder(orderId: string) {
      
        this.isdelete = true;
       
        this.orderService.deleteOrders(orderId).subscribe(() => {
           
            let index = this.orders.findIndex(o => o.id === orderId);
            this.pagedItems.splice(index, 1);
          
        },err=>console.log(err));
    }
 


 

}
