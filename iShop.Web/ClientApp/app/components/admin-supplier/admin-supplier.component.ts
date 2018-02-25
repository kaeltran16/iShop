 
import { Component, OnInit,Input, TemplateRef } from '@angular/core';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import { UserService } from '../../service/user.service';
import { SupplierService } from '../../service/supplier.service';

@Component({
    
    selector: 'admin-supplier',
    templateUrl: './admin-supplier.component.html',
    styleUrls: ['./admin-supplier.component.css']
})
export class AdminSupplierComponent implements OnInit {
    modalRef: BsModalRef;
    suppliers:any[]=[];
    constructor(
 
        private modalService: BsModalService,
        private  supplierService:SupplierService,
        private userService: UserService,
     
    ) { }
   
   

    ngOnInit() {
        let token = localStorage.getItem("token");
        token ? this.supplierService.getSuppliers(token).subscribe(s => {
                this.suppliers = s;
            }
            ): alert("Bạn không đủ quyền truy cập vào mục này");
    }


    exitDetail(supplier: any) {
        if (supplier) {
            this.modalRef.hide();
            if (typeof supplier === 'boolean') {
                return;
            }
            console.log(supplier);
            let index: number = this.suppliers.findIndex(s => s.id === supplier);
            console.log(index);
            if (index !==-1) {
              
                this.suppliers.splice(index,1);
                return;
            }
            this.suppliers.push(supplier);
        }
    }


    
  
    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template);
    }
}
