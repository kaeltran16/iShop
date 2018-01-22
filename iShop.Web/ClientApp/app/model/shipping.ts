export class Shipping {
    shippingState: number;
    charge: number;
    ward: string;
    district: string;
    city: string;
    userName:string;
    phoneNumber: string;
    orderId: string;
    
    constructor(shippingState: number, charge: number, ward: string, district: string, city: string, userName: string, phoneNumber:string,order:string) {
        this.shippingState = shippingState;
        this.charge = charge;
        this.ward = ward;
        this.district = district;
        this.city = city;
        this.userName = userName;
        this.phoneNumber = phoneNumber;
        this.orderId = order;
    }
}