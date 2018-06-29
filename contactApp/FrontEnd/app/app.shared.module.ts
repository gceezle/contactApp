import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ContactComponent } from './components/contacts/contacts.component';
import { AddContactComponent } from './components/add-contact/add-contact.component';

import { ContactService } from './services/contact.service';

import { AppRoutes } from './app.routing';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        ContactComponent,
        HomeComponent,
        AddContactComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot(AppRoutes)
    ],
    providers: [
        ContactService
    ]
})
export class AppModuleShared {
}
