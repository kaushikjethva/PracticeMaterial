import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private form:FormGroup;
  constructor(private fb: FormBuilder, private authSVC:AuthService,private router:Router ) { }

  ngOnInit() {
    this.form = this.fb.group({
      "email":["",Validators.required],
      "password":["",Validators.required]
    })
  }

  submit(){
    if(this.form.valid){
      let token = "";      
      this.authSVC.getToken(this.form.value)
      .subscribe(        
        result=>{           
          localStorage.setItem("auth-token",result);
          this.router.navigate(["/"]);
        },
        err=>{console.log("Error:", err)}
      )
    }
    else{
      alert("Invalid Values.");
    }
  }

}
