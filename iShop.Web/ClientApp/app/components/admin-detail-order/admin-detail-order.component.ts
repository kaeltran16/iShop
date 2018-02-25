import { Component, Output, EventEmitter, Input, OnInit, TemplateRef } from "@angular/core";
import { ShippingService } from "../../service/shipping.service";
import { OrderService } from "../../service/order.service";



@Component({
    selector: "admin-detail-order",
    templateUrl: "./admin-detail-order.component.html",
    styleUrls: ["./admin-detail-order.component.css"]
})
export class AdminDetailOrderComponent implements OnInit {
    ngOnInit(): void {

     

    } 
    constructor(private shippingService: ShippingService,
    private orderService:OrderService) { }
    @Output() onclick = new EventEmitter<any>();
    @Input('order') order: any;
 
    editState() {
     
        let token = localStorage.getItem("token");
        token
            ? this.shippingService.updateShipping(this.order.shipping, token).subscribe(s => {this.onclick.emit(true);},err=>alert("Đơn đặt hàng chưa cập nhật"))
            : alert("Bạn không đủ quyền truy cập vào mục này");
    }


    deleteOrder() {
        this.orderService.deleteOrders(this.order.id).subscribe(() => {
            this.onclick.emit(this.order.id);
        }, err => console.log(err));
    }


    }
   


   


  

   



