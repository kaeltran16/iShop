import { Cart } from "./Cart";


export class ShoppingCart {
   
    userId: string;
    carts: Cart[]=[];
    constructor( userId: string, carts: Cart[]) {
      
        this.userId = userId;
        this.carts = carts;
    }
}

