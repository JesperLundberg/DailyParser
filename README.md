# DailyParser

Parses markdown files according to need. The information is stored in a database and then presented from an API. There is also a frontend that consumes the API.

== Note that this is still a work in progress and so far it's very tailored to _my_ needs ==.

## How to run

Run `sudo docker-compose` in solution root.

Default configuration puts API on localhost:5000 and Frontend on localhost:3000.

## TODO

- [X] Transition to alpine container for backend (ubuntu is too big)
- [X] Fix Frontend docker container
- [X] Use parallellism when reading the files from disk
- [X] Make the API more RESTful
- [X] Make sure frontend works in the container, it seems ok but maybe not?
- [X] Parser can't yet be triggered, must be triggerable
- [X] Parser does not replace game info, it adds which gives doubles, tripples etc
- [ ] Consolidate the namespaces? At least take a look and decide on what to do!
- [ ] Sort out the development and prod appsettings. Use prod in docker.
- [ ] Update days and games in parallell and async the same way files are read from disk (in filesystem repository)
- [ ] Get Game data from Steam (total played time)
- [ ] Use HyperUI components for frontend (https://www.hyperui.dev/)
