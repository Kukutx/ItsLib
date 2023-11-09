import { TestBed } from '@angular/core/testing';

import { ProductUserService } from './product.user.service';

describe('ProductUserService', () => {
  let service: ProductUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
