import { Component, Output, EventEmitter, Input, OnInit } from "@angular/core";



import { Supplier } from "../../model/Supplier";
import { SupplierService } from "../../service/supplier.service";


@Component({
    selector: "admin-edit-supplier",
    templateUrl: "./admin-edit-supplier.component.html",
    styleUrls: ["./admin-edit-supplier.component.css"]
})
export class AdminEditSupplierComponent implements OnInit {
    ngOnInit(): void {



    }


    @Input('supplier') supplier: Supplier;
    @Output() onclick = new EventEmitter<any>();



    editSupplier($event: any) {
        $event._submitted = true;
        console.log(this.supplier);
        if ($event.valid) {
            let token = localStorage.getItem("token");
            token
                ? this.supplierService.editSuppliers(token, this.supplier).
                subscribe(s => this.onclick.emit(true), err => console.log(err)) : alert("Bạn không đủ quyền vào mục này");
        }
    }

    deleteSupplier() {
       console.log(this.supplier);
            let token = localStorage.getItem("token");
            token
                ? this.supplierService.deleteSuppliers(token, this.supplier.id).
                    subscribe(s => this.onclick.emit(this.supplier.id), err => alert("Nhà cung câp này đang sử dụng nên bạn không thể xóa"))
                : alert("Bạn không đủ quyền vào mục này");
    }


    constructor(
        private supplierService: SupplierService,
    ) {


    }







}
