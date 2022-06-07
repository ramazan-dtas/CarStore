import { customer } from './customer';
import { role } from './role';
import { order } from './order';

export interface user {
    id? : number,
    email? : string,
    password? : string,
    role? : role,
    orderList? : order[],
    customer? : customer[] 
}