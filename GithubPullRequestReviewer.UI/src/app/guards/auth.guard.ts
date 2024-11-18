import { inject } from '@angular/core';
import { CanActivateChildFn, RedirectCommand, Router } from '@angular/router';
import { LocalStorageService } from '../services/local-storage.service';
import { TOKEN_STORAGE_KEY } from '../common/constants';

export const authGuard: CanActivateChildFn = (childRoute, state) => {
  const localStorageService = inject(LocalStorageService);
  if (localStorageService.getItem(TOKEN_STORAGE_KEY)) {
    return true;
  } else {
    const router = inject(Router);
    const authUrlTree = router.createUrlTree(['/auth']);
    return new RedirectCommand(authUrlTree);
  }
};
