import { Product } from "./product";

export interface ODataProductResponse {
    context: string,
    count: number,
    value: Product[]
}
