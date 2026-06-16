FROM debian:trixie

RUN apt-get update
RUN apt-get install -y libicu-dev

COPY --chown=1000:1000 ./network-monitor/bin/Release/net10.0/linux-x64/publish /deployment

CMD [ "/deployment/network-monitor", "/db" ]