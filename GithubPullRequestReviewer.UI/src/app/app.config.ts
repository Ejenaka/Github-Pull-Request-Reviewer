import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient } from '@angular/common/http';
import { ApiModule as PullRequestApiModule } from './api/pull-request/api.module';
import { ApiModule as EventHandlerApiModule } from './api/event-handler/api.module';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),
    importProvidersFrom(PullRequestApiModule.forRoot({ rootUrl: 'http://localhost:5124' })),
    importProvidersFrom(EventHandlerApiModule.forRoot({ rootUrl: 'http://localhost:5226' })),
  ]
};
