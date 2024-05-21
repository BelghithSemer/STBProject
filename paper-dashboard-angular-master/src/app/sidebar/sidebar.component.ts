import { Component, OnInit } from '@angular/core';
import { Routes } from '@angular/router';


export interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}

export const ROUTES: RouteInfo[] = [
    { path: '/dashboard',    title: 'Dashboard',         icon:'nc-bank',       class: '' },
    { path: '/simulateur',       title: 'Simulateur du crédit',             icon:'nc-diamond',    class: '' },
    { path: '/demande',         title: 'Demande du crédit',              icon:'nc-pin-3',      class: '' },
 
    { path: '/user',        title: 'Mon Profile',      icon:'nc-single-02',  class: '' },


    { path: '/upgrade',     title: 'Upgrade to PRO',    icon:'nc-spaceship',  class: 'active-pro' },
];

@Component({
    moduleId: module.id,
    selector: 'sidebar-cmp',
    templateUrl: 'sidebar.component.html',
})

export class SidebarComponent implements OnInit {
    public menuItems: any[];
    ngOnInit() {
        this.menuItems = ROUTES.filter(menuItem => menuItem);
    }
}
