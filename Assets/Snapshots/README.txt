This file is here so git stops ignoring this folder while it is empty. Git ignores empty
folders but will not ignore the Snapshots.meta file created by Unity. So, each time Git
is used to pull down the repo, the Snapshots.meta file will notice that the Snapshots
folder is gone (because git didn't track it since it was empty) and will create a new
Snapshots folder and thus create a new GUID. So, everyone's meta files will be constantly
changing.

- Wesley
