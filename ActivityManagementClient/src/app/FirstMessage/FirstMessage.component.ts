import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ResultModel } from '../FirstMessage/ResultModel';
 
 

@Component({
  selector: 'app-FirstMessage',
  templateUrl: './FirstMessage.component.html',
  styleUrls: ['./FirstMessage.component.css']
})
export class FirstMessageComponent implements OnInit {

   
  resultModel: ResultModel;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getMessage();
  }

  getMessage() {
    this.http.get<ResultModel>('http://localhost:9788/api/v1/Home').subscribe(res => {
 
      this.resultModel = res;
      
    }, error => {
     
      console.log(error);

    });
  }
}
