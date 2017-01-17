@echo ==========================================================================
@echo URL RESERVATION
@echo --------------------------------------------------------------------------
@echo url=http://+:8465/AfxResourceDirectory
@echo user=%USERDOMAIN%\%USERNAME%
@netsh http add urlacl url=http://+:8465/AfxResourceDirectory user=%USERDOMAIN%\%USERNAME%
@echo ==========================================================================
