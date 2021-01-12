/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserGridService } from '../components/user/services/User.Grid.service';

describe('Service: UserGridService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserGridService]
    });
  });

  it('should ...', inject([UserGridService], (service: UserGridService) => {
    expect(service).toBeTruthy();
  }));
});
