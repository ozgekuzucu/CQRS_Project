import { Link, List, Stack, Toolbar, Typography } from '@mui/material';
import { drawerItems } from 'data/drawerItems';
import Logo from 'components/icons/common/Logo';
import { rootPaths } from 'routes/paths';
import DrawerListItem from './DrawerListItem';

const DrawerList = () => {
  return (
    <div>
      <Toolbar disableGutters>
        <Stack
          component={Link}
          href={rootPaths.root}
          direction="row"
          alignItems="center"
          columnGap={1.5}
        >
          <Logo sx={{ fontSize: 27 }} />
          <Typography variant="h2" component="h1" sx={{ color: 'neutral.darker' }}>
            Motiv.
          </Typography>
        </Stack>
      </Toolbar>

      <Stack
        sx={(theme) => ({
          height: `calc(100vh - ${theme.mixins.toolbar.minHeight}px)`,
          p: theme.spacing(2, 3),
          justifyContent: 'space-between',
          overflowY: 'auto',
        })}
      >
        <List sx={{ pt: 0 }}>
          {drawerItems.slice(0, -2).map((drawerItem) => (
            <DrawerListItem key={drawerItem.id} item={drawerItem} />
          ))}
        </List>

        <List>
          {drawerItems.slice(-2).map((drawerItem) => (
            <DrawerListItem key={drawerItem.id} item={drawerItem} />
          ))}
        </List>
      </Stack>
    </div>
  );
};

export default DrawerList;
