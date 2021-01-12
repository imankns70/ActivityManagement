import { Injectable, ViewChild, ViewContainerRef } from '@angular/core';
import { WindowRef, WindowService } from '@progress/kendo-angular-dialog';

@Injectable({
  providedIn: 'root'
})
export class SharedService {



  constructor(private windowService: WindowService) { }


  public renderWindow(viewContainerRef :ViewContainerRef,content: any, title: string, width?:number): WindowRef {
    return this.windowService.open({

      appendTo: viewContainerRef,
      title: title,
      content: content,
      width: width ? width : 600
    })
   

  }
}
