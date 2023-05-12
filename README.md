# DailyParser

Parses markdown files according to need. The information is stored in a database and then presented from an API. There is also a frontend that consumes the API.

== Note that this is still a work in progress and so far it's very tailored to _my_ needs ==.

## How to run

Run `sudo docker-compose` in solution root.

## TODO

- [X] Transition to alpine container for backend (ubuntu is too big)
- [X] Fix Frontend docker container
- [X] Use parallellism when reading the files from disk
- [X] Make the API more RESTful
- [ ] Make sure frontend works in the container, it seems ok but maybe not?
- [ ] Parser can't yet be triggered, must be triggerable
- [ ] Consolidate the namespaces? At least take a look and decide on what to do!
