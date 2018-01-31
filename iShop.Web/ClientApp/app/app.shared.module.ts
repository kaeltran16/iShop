import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { RatingModule } from 'ngx-bootstrap/rating';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DatePipe } from '@angular/common';
//service 
import { ProductService } from "./service/product.service";
import { UserService } from "./service/user.service";
import { SharedService } from "./service/shared-service";
import { OrderService } from "./service/order.service";
import { PagerService } from "./service/page.service";
import { CategoryService } from "./service/category.service";
import { ShippingService } from "./service/shipping.service";
import { SupplierService } from "./service/supplier.service";
import { ImageService } from "./service/image.service";
import { AdminAuthGuardService } from "./service/admin-auth-guard.service";

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { DetailProductComponent } from './components/detail-product/detail-product.component';
import { MoreProductComponent } from './components/more-product/more-product.component';
import { CardComponent } from './components/card/card.component';
import { ItemMenuComponent } from './components/item-menu/item-menu.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FooterComponent } from './components/footer/footer.component';
import { CartComponent } from './components/cart/cart.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { SlideImageComponent } from './components/slide-image/slide-image.component';
import { OrderComponent } from './components/order/order.component';
import { ListProductComponent } from './components/list-product/list-product.component';
import { ListProductRandomComponent } from './components/list-product-random/list-product-random.component';
import { AdminComponent } from './components/admin/admin.component';
import { AdminMenuComponent } from './components/admin-navmenu/admin-navmenu.component';
import { CustomerComponent } from './components/customer/customer.component';
import { DashbroadComponent } from './components/dashbroad/dashboard.component';
import { AdminProductComponent } from './components/admin-product/admin-product.component';
import { AdminCardComponent } from './components/admin-card/admin-card.component';
import { AdminEditProductComponent } from './components/admin-edit-product/admin-edit-product.component';
import { AdminCreateProductComponent } from './components/admin-create-product/admin-create-product.component';
import { AdminCreatecCategoryComponent } from './components/admin-create-category/admin-create-category.component';
import { AdminCategoryComponent } from './components/admin-category/admin-category.component';
import { AdminEditcCategoryComponent } from './components/admin-edit-category/admin-edit-category.component';
import { AdminOrderComponent } from './components/admin-order/admin-order.component';
import { AdminSupplierComponent } from './components/admin-supplier/admin-supplier.component';

// custom validation 
import { EqualValidator } from './components/register/custom-validation';

//custom pipe
import { FilterPipe } from './components/custom-pipe/filter-pipe';
import { FillPipe } from './components/custom-pipe/fill-pipe';
import { FilterCategoryPipe } from './components/custom-pipe/filter-category-pipe';



@NgModule({
    declarations: [
        //component
        EqualValidator,
        AppComponent,
        NavMenuComponent,
        DetailProductComponent,
        MoreProductComponent,
        CardComponent,
        ItemMenuComponent,
        LoginComponent,
        RegisterComponent,
        HomeComponent,
        FooterComponent,
        CartComponent,
        ShoppingCartComponent,
        SlideImageComponent,
        OrderComponent,
        ListProductComponent,
        ListProductRandomComponent,
        CustomerComponent,
        DashbroadComponent,
        //pipe
        FilterPipe,
        FillPipe,
        FilterCategoryPipe,
       

        // admin
        AdminComponent,
        AdminMenuComponent,
        AdminCardComponent,
        AdminEditProductComponent,
        AdminProductComponent,
        AdminCreateProductComponent,
        AdminCreatecCategoryComponent,
        AdminCategoryComponent,
        AdminEditcCategoryComponent,
        AdminOrderComponent,
        AdminSupplierComponent
       
    ],
    imports: [
        //ngx bootstrap 
        RatingModule.forRoot(),
        AccordionModule.forRoot(),
        CarouselModule.forRoot(),
        ModalModule.forRoot(),
        PopoverModule.forRoot(),
        TabsModule.forRoot(),
        BsDatepickerModule.forRoot(),

        //angular 
        BrowserAnimationsModule,
        CommonModule,
        HttpModule,
        FormsModule,
      
        //path
        RouterModule.forRoot([
            {
                //admin
                path: 'admin', component: AdminComponent, canActivate: [AdminAuthGuardService], children: [
                    { path: '', redirectTo: 'dashbroad', pathMatch: 'full', canActivate: [AdminAuthGuardService] },
                    { path: 'dashbroad', component: DashbroadComponent, canActivate: [AdminAuthGuardService] },
                    { path: 'admin-product', component: AdminProductComponent, canActivate: [AdminAuthGuardService] },
                    { path: 'admin-category', component: AdminCategoryComponent, canActivate: [AdminAuthGuardService] },
                    { path: 'admin-order', component: AdminOrderComponent, canActivate: [AdminAuthGuardService] },
                    { path: 'admin-supplier', component: AdminOrderComponent, canActivate: [AdminAuthGuardService] },
                    { path: '**', redirectTo: 'dashbroad', canActivate: [AdminAuthGuardService] }
                ]
            },
            {
                //customer
                path: '',
                component: CustomerComponent,   
                children: [
                    { path: '', redirectTo: 'home', pathMatch: 'full' },
                    { path: 'home', component: HomeComponent },
                    { path: 'more-product', component: MoreProductComponent },
                    { path: 'more-product/:title', component: MoreProductComponent },
                    { path: 'shopping-cart', component: ShoppingCartComponent },
                    { path: 'order', component: OrderComponent },
                    { path: '**', redirectTo: 'home' }
                ]
            }

         
             
            //admin
         
        ])
    ],
    providers: [
        //service
        ProductService,
        UserService,
        SharedService,
        OrderService,
        PagerService,
        CategoryService,
        ShippingService,
        SupplierService,
        ImageService,
        AdminAuthGuardService,
        DatePipe
    ]
})
export class AppModuleShared {
}
