/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TokenUserService } from './token-user.service';

describe('Service: TokenUser', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TokenUserService]
    });
  });

  it('should ...', inject([TokenUserService], (service: TokenUserService) => {
    expect(service).toBeTruthy();
  }));
});
