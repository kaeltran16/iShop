import { Cart } from "./Cart";


export class ShoppingCart {
    id: string;
    userId: string;
    carts: Cart[];
    constructor(id: string, userId: string, carts: Cart[]) {
        this.id = id;
        this.userId = userId;
        this.carts = carts;
    }
}

