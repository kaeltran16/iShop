export class Shipping {
    shippingState: string;
    charge: number;
    ward: string;
    disctrict: string;
    city: string;
    name:string;

    constructor(shippingState: string, charge: number, ward: string, disctrict: string, city: string, name: string) {
        this.shippingState = shippingState;
        this.charge = charge;
        this.ward = ward;
        this.disctrict = disctrict;
        this.city = city;
        this.name = name;
    }
}