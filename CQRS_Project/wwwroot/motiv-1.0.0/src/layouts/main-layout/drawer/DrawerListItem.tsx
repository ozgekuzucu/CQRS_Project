import { Link, ListItem, ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import IconifyIcon from 'components/base/IconifyIcon';
import { useState } from 'react';
import { DrawerItem } from 'types/types';
import CollapsedItems from './CollapsedItems';
import { useLocation } from 'react-router-dom';

const DrawerListItem = ({ item }: { item: DrawerItem }) => {
  const location = useLocation();
  const [open, setOpen] = useState(false);

  const handleCollapsedItem = () => {
    setOpen(!open);
  };

  const { icon: Icon, title, path, collapsible, active } = item;

  return (
    <ListItem
      disablePadding
      sx={{
        flexDirection: 'column',
        alignItems: 'stretch',
        mb: 1.25,
        opacity: active ? 1 : 0.5,
        width: 174,
      }}
    >
      <ListItemButton
        selected={location.pathname === path}
        onClick={handleCollapsedItem}
        component={path ? Link : 'div'}
        href={path}
      >
        <ListItemIcon sx={{ mr: 1 }}>
          <Icon fontSize="small" sx={{ color: 'grey.600' }} />
        </ListItemIcon>
        <ListItemText primary={title} />
        {collapsible && <IconifyIcon icon={open ? 'ep:arrow-up' : 'ep:arrow-down'} />}
      </ListItemButton>

      {collapsible && <CollapsedItems subItems={item.subList} open={open} />}
    </ListItem>
  );
};

export default DrawerListItem;
