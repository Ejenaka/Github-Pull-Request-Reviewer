import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { User } from '../../api/models';
import { UserService } from '../../api/services';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  user: User;

  constructor(
    private readonly userApiService: UserService,
    private readonly authService: AuthService) { }

  ngOnInit() {
    this.userApiService.apiUsersCurrentGet$Json({ access_token: this.authService.getAccessToken() }).subscribe(user => this.user = user);
  }

}
