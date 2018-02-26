import { Component, Output, EventEmitter, Input, OnInit } from "@angular/core";



import { Supplier } from "../../model/Supplier";
import { SupplierService } from "../../service/supplier.service";


@Component({
    selector: "admin-create-supplier",
    templateUrl: "./admin-create-supplier.component.html",
    styleUrls: ["./admin-create-supplier.component.css"]
})
export class AdminCreateSupplierComponent implements OnInit {
    ngOnInit(): void {

     

    } 

 
   supplier: Supplier=new Supplier() ;
   @Output() onclick = new EventEmitter<Supplier>();
 


    createSupplier($event: any) {
        $event._submitted = true;
        console.log(this.supplier);
        if ($event.valid) {
            let token = localStorage.getItem("token");
            token
                ? this.supplierService.createSuppliers(token, this.supplier).
                    subscribe(s => this.onclick.emit(s), err => console.log(err)) : alert("Bạn không đủ quyền vào mục này");
        }
    }


    constructor(
        private supplierService: SupplierService,
        ) {
     

    }


  

   


}
