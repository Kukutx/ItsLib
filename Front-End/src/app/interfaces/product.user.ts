import { Product } from "./product";
import { UserOp } from "./user.op";

export interface ProductUser {
    userId: string,
    productId: string,
    inWishList: boolean,
    isUsed: boolean,
    review: string | null,
    reviewTitle: string | null,
    isDisabled: boolean,
    dateAdded: Date,
    lastModifiedDate: Date,
    user: UserOp | undefined,
    product: Product | undefined
}
