import { Product } from "./product"

export interface Category {
    categoryId: string,
    name: string,
    isDisabled: boolean,
    color: string,
    icon: string,
    img: string
    products: Product[] | undefined
}
