import { AppBar, IconButton, Link, Stack, Toolbar } from '@mui/material';
import IconifyIcon from 'components/base/IconifyIcon';
import AccountMenu from './AccountMenu';
import LanguagePopover from './LanguagePopover';

import OutlinedBadge from 'components/styled/OutlinedBadge';
import SearchBox from 'components/common/SearchBox';
import { rootPaths } from 'routes/paths';
import Logo from 'components/icons/common/Logo';
import ElevationScroll from './ElevationScroll';
import Search from 'components/icons/common/Search';
import Notification from 'components/icons/appbar/Notification';

interface TopBarProps {
  drawerWidth: number;
  onHandleDrawerToggle: () => void;
}

const TopBar = ({ drawerWidth, onHandleDrawerToggle }: TopBarProps) => {
  return (
    <ElevationScroll>
      <AppBar
        position="fixed"
        sx={{
          width: { md: `calc(100% - ${drawerWidth + 1}px)` },
          ml: { sm: `${drawerWidth}px` },
        }}
      >
        <Toolbar sx={{ justifyContent: 'space-between' }}>
          <Stack
            direction="row"
            alignItems="center"
            columnGap={{ xs: 1, sm: 2 }}
            sx={{ display: { md: 'none' } }}
          >
            <Link href={rootPaths.root}>
              <IconButton color="inherit" aria-label="logo">
                <Logo sx={{ fontSize: 27 }} />
              </IconButton>
            </Link>

            <IconButton color="inherit" aria-label="open drawer" onClick={onHandleDrawerToggle}>
              <IconifyIcon icon="mdi:hamburger-menu" />
            </IconButton>

            <IconButton color="inherit" aria-label="search-icon">
              <Search fontSize="small" />
            </IconButton>
          </Stack>

          <SearchBox />

          <Stack direction="row" alignItems="center" columnGap={{ xs: 1, sm: 2, md: 3 }}>
            <LanguagePopover />
            <IconButton aria-label="notifications" color="inherit">
              <OutlinedBadge badgeContent=" " color="error" variant="dot" overlap="circular">
                <Notification />
              </OutlinedBadge>
            </IconButton>
            <AccountMenu />
          </Stack>
        </Toolbar>
      </AppBar>
    </ElevationScroll>
  );
};

export default TopBar;
