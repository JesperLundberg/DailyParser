# DailyParser

Parses markdown files according to need. The information is stored in a database and then presented from an API. There is also a frontend that consumes the API.

== Note that this is still a work in progress and so far it's very tailored to _my_ needs ==.

## How to run

Run `sudo docker-compose` in solution root.

## TODO

- [ ] Fix Frontend docker container
- [ ] Parser can't yet be triggered, must be triggerable
- [X] Transition to alpine container for backend (ubuntu is too big)
- [ ] Make the API more RESTful
- [ ] Consolidate the namespaces? At least take a look and decide on what to do!
