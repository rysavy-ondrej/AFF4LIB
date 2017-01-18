@echo ==========================================================================
@echo URL RESERVATION
@echo --------------------------------------------------------------------------
@echo url=http://+:8465/afx/resourceDirectory
@echo user=%USERDOMAIN%\%USERNAME%
@netsh http add urlacl url=http://+:8465/afx/resourceDirectory user=%USERDOMAIN%\%USERNAME%
@echo ==========================================================================
